#!/usr/bin/env python
"""
Professional Resume Analyzer with Model-Based Skill Extraction and Context-Aware Matching

This script extracts skills from a resume and a job description using a transformer-based spaCy model.
It supports PDF and DOCX formats for the resume. Skills are extracted via model-based NER
(with a fallback to generic noun-chunk extraction) and semantic similarity is computed at the sentence level.
Usage:
    python python_script.py <resume_file> <job_description>
"""

import sys
import os
import logging
from collections import defaultdict

import docx
import numpy as np
import PyPDF2
import spacy
from spacy.tokens import Doc, Span

# Configure professional logging (both to file and console)
logging.basicConfig(
    level=logging.INFO,
    format="%(asctime)s [%(levelname)s] %(name)s - %(message)s",
    handlers=[logging.FileHandler("analysis.log"), logging.StreamHandler()]
)
logger = logging.getLogger(__name__)


class ProfessionalResumeAnalyzer:
    """
    Resume analysis engine that uses a transformer-based spaCy model for skill extraction
    and computes contextual similarity between resume and job description texts.
    """

    def __init__(self, model_name: str = "en_core_web_trf"):
        """
        Initialize the analyzer by loading the specified spaCy model.
        :param model_name: Name of the spaCy model (default: en_core_web_trf)
        """
        try:
            self.nlp = spacy.load(model_name)
            logger.info("Loaded NLP model: %s", model_name)
        except Exception as e:
            logger.critical("Failed to load NLP model '%s': %s", model_name, e)
            raise

    def _extract_text(self, file_path: str) -> str:
        """
        Extract text from the resume file.
        Supported formats: PDF and DOCX.
        :param file_path: Path to the resume file.
        :return: Extracted text.
        """
        try:
            if file_path.lower().endswith(".pdf"):
                return self._extract_pdf(file_path)
            elif file_path.lower().endswith(".docx"):
                return self._extract_docx(file_path)
            else:
                raise ValueError(f"Unsupported file format: {file_path}")
        except Exception as e:
            logger.error("Text extraction failed: %s", e)
            raise

    def _extract_pdf(self, path: str) -> str:
        """
        Extract text from a PDF file.
        :param path: PDF file path.
        :return: Extracted text.
        """
        text_parts = []
        try:
            with open(path, "rb") as f:
                reader = PyPDF2.PdfReader(f)
                if reader.is_encrypted:
                    try:
                        reader.decrypt("")
                    except Exception as de:
                        logger.error("Encrypted PDF not supported: %s", path)
                        raise de
                for page in reader.pages:
                    page_text = page.extract_text()
                    if page_text:
                        text_parts.append(page_text)
            return "\n".join(text_parts)
        except Exception as e:
            logger.error("PDF extraction error for '%s': %s", path, e)
            raise

    def _extract_docx(self, path: str) -> str:
        """
        Extract text from a DOCX file.
        :param path: DOCX file path.
        :return: Extracted text.
        """
        try:
            doc = docx.Document(path)
            return "\n".join([p.text for p in doc.paragraphs if p.text])
        except Exception as e:
            logger.error("DOCX extraction error for '%s': %s", path, e)
            raise

    def _extract_skills(self, doc: Doc) -> dict:
        """
        Extract skills from the provided spaCy Doc using model-based NER.
        Falls back to generic extraction using noun chunks if no skills are detected.
        :param doc: spaCy processed document.
        :return: Dictionary mapping skill names to a list of occurrences (tuple of detected text and sentence).
        """
        skills = defaultdict(list)
        for ent in doc.ents:
            if ent.label_ in ["SKILL", "TECH"]:
                skills[ent.text].append((ent.text, ent.sent.text))
        if not skills:
            skills = self._generic_extract_skills(doc)
            logger.info("No model-based skills found; falling back to generic extraction.")
        return skills

    def _generic_extract_skills(self, doc: Doc) -> dict:
        """
        Fallback method for skill extraction using noun chunks and simple heuristics.
        :param doc: spaCy processed document.
        :return: Dictionary mapping potential skills to occurrences.
        """
        skills = defaultdict(list)
        for chunk in doc.noun_chunks:
            if len(chunk.text) > 1 and any(token.pos_ in ["NOUN", "PROPN"] for token in chunk):
                skills[chunk.text.strip()].append((chunk.text, chunk.sent.text))
        return skills

    def _calculate_semantic_similarity(self, resume_doc: Doc, job_doc: Doc) -> tuple:
        """
        Calculate a sentence-level semantic similarity matrix between resume and job description.
        :param resume_doc: spaCy Doc for the resume.
        :param job_doc: spaCy Doc for the job description.
        :return: Tuple containing (similarity matrix, list of resume sentences, list of job sentences).
        """
        resume_sents = list(resume_doc.sents)
        job_sents = list(job_doc.sents)
        matrix = np.zeros((len(resume_sents), len(job_sents)))
        for i, r_sent in enumerate(resume_sents):
            for j, j_sent in enumerate(job_sents):
                matrix[i][j] = r_sent.similarity(j_sent)
        return matrix, resume_sents, job_sents

    def analyze(self, resume_path: str, job_desc_text: str) -> dict:
        """
        Main analysis workflow.
        :param resume_path: Path to the resume file.
        :param job_desc_text: Job description text.
        :return: A report dictionary with extracted skills, semantic similarity, and matches.
        """
        try:
            resume_text = self._extract_text(resume_path)
            resume_doc = self.nlp(resume_text)
            job_doc = self.nlp(job_desc_text)

            resume_skills = self._extract_skills(resume_doc)
            job_skills = self._extract_skills(job_doc)

            sim_matrix, resume_sents, job_sents = self._calculate_semantic_similarity(resume_doc, job_doc)
            report = self._generate_report(resume_skills, job_skills, sim_matrix, resume_sents, job_sents)
            return report
        except Exception as e:
            logger.error("Analysis process encountered an error: %s", e)
            raise

    def _generate_report(self, resume_skills: dict, job_skills: dict,
                         sim_matrix: np.ndarray, resume_sents: list, job_sents: list) -> dict:
        """
        Generate a comprehensive match report.
        :param resume_skills: Skills extracted from the resume.
        :param job_skills: Skills extracted from the job description.
        :param sim_matrix: Sentence similarity matrix.
        :param resume_sents: List of sentences from the resume.
        :param job_sents: List of sentences from the job description.
        :return: Dictionary with exact matches, missing skills, contextual matches, and skill frequency.
        """
        report = {
            "exact_matches": [],
            "context_matches": [],
            "missing_skills": [],
            "skill_frequency": {skill: len(occurrences) for skill, occurrences in resume_skills.items()},
            "similarity_matrix": sim_matrix.tolist()  # for serialization if needed
        }
        # Determine exact matches and missing skills
        for skill in job_skills:
            if skill in resume_skills:
                report["exact_matches"].append(skill)
            else:
                report["missing_skills"].append(skill)
        # Identify contextual matches (top 15th percentile similarity)
        if sim_matrix.size > 0:
            threshold = np.percentile(sim_matrix, 85)
            indices = np.where(sim_matrix > threshold)
            for i, j in zip(*indices):
                report["context_matches"].append({
                    "resume_sentence": resume_sents[i].text,
                    "job_sentence": job_sents[j].text,
                    "similarity": float(sim_matrix[i][j])
                })
        return report


def main():
    if len(sys.argv) < 3:
        print("Usage: python python_script.py <resume_file> <job_description>")
        sys.exit(1)
    resume_file = sys.argv[1]
    job_description = sys.argv[2]
    analyzer = ProfessionalResumeAnalyzer()
    report = analyzer.analyze(resume_file, job_description)
    # For this example, we output a suggested position based on the first exact skill match.
    suggested = report["exact_matches"][0] if report["exact_matches"] else "No direct skill match found."
    print(suggested)


if __name__ == "__main__":
    main()
