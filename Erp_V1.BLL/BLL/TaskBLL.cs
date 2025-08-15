﻿// File: TaskBLL.cs
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    public class TaskBLL : IBLL<TaskDetailDTO, TaskDTO>
    {
        private readonly TaskDAO _dao = new TaskDAO();
        private readonly EmployeeDAO _emp = new EmployeeDAO();
        private readonly DepartmentDAO _dept = new DepartmentDAO();
        private readonly PositionDAO _pos = new PositionDAO();

        public bool Insert(TaskDetailDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.EmployeeID <= 0) throw new ArgumentException("A valid employee must be assigned.", nameof(dto.EmployeeID));
            if (string.IsNullOrWhiteSpace(dto.Title)) throw new ArgumentException("Task title cannot be empty.", nameof(dto.Title));

            var entity = new TASK
            {
                TaskTitle = dto.Title,
                TaskContent = dto.Content,
                TaskStartDate = dto.TaskStartDate ?? DateTime.Today,
                EmployeeID = dto.EmployeeID,

                // ** THIS IS THE FIX: The default state for a new task is now 'Delivered' **
                TaskState = TaskStates.Delivered
            };
            return _dao.Insert(entity);
        }

        public bool Update(TaskDetailDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.TaskID <= 0) throw new ArgumentException("A valid TaskID is required for an update.", nameof(dto.TaskID));
            if (dto.EmployeeID <= 0) throw new ArgumentException("A valid employee must be assigned.", nameof(dto.EmployeeID));
            if (string.IsNullOrWhiteSpace(dto.Title)) throw new ArgumentException("Task title cannot be empty.", nameof(dto.Title));

            var entity = new TASK
            {
                ID = dto.TaskID,
                TaskTitle = dto.Title,
                TaskContent = dto.Content,
                TaskState = dto.taskStateID, // For an update, we trust the existing state from the DTO
                EmployeeID = dto.EmployeeID
            };
            return _dao.Update(entity);
        }

        #region Unchanged Methods
        public bool Delete(TaskDetailDTO dto)
            => _dao.Delete(new TASK { ID = dto.TaskID });

        public bool GetBack(TaskDetailDTO dto)
            => _dao.GetBack(dto.TaskID);

        public TaskDTO Select()
        {
            return new TaskDTO
            {
                Employees = _emp.Select(),
                Departments = _dept.Select(),
                Positions = _pos.Select(),
                TaskStates = _dao.GetStates(),
                Tasks = _dao.Select()
            };
        }

        public bool Approve(TaskDetailDTO dto, bool isAdmin)
            => _dao.Approve(dto.TaskID, isAdmin);
        #endregion
    }
}