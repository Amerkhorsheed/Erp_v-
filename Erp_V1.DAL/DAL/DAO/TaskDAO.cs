// File: TaskDAO.cs
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAL;


namespace Erp_V1.DAL.DAO
{
    public class TaskDAO : StockContext, IDAO<TASK, TaskDetailDTO>
    {
        public bool Insert(TASK entity)
        {
            try
            {
                DbContext.TASK.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception($"Task insertion failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        public bool Update(TASK entity)
        {
            try
            {
                var t = DbContext.TASK.First(x => x.ID == entity.ID);
                t.TaskTitle = entity.TaskTitle;
                t.TaskContent = entity.TaskContent;
                t.TaskState = entity.TaskState;
                t.EmployeeID = entity.EmployeeID;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception($"Task update failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        public List<TaskDetailDTO> Select() => Select(false);

        public List<TaskDetailDTO> Select(bool includeDeleted)
        {
            var q = from t in DbContext.TASK
                    join s in DbContext.TASKSTATE on t.TaskState equals s.ID
                    // Use LEFT JOINs for all related entities for robustness
                    join e_join in DbContext.EMPLOYEE on t.EmployeeID equals e_join.ID into e_group
                    from e in e_group.DefaultIfEmpty()
                    join d_join in DbContext.DEPARTMENT on e.DepartmentID equals d_join.ID into d_group
                    from d in d_group.DefaultIfEmpty()
                    join p_join in DbContext.POSITION on e.PositionID equals p_join.ID into p_group
                    from p in p_group.DefaultIfEmpty()
                    orderby t.TaskStartDate
                    select new TaskDetailDTO
                    {
                        TaskID = t.ID,
                        Title = t.TaskTitle,
                        Content = t.TaskContent,
                        TaskStartDate = t.TaskStartDate,
                        TaskDeliveryDate = t.TaskDeliveryDate,
                        taskStateID = t.TaskState,
                        TaskStateName = s.StateName,
                        EmployeeID = e != null ? e.ID : 0,
                        UserNo = e != null ? e.UserNo : 0,
                        Name = e != null ? e.Name : "Unassigned",
                        Surname = e != null ? e.Surname : "",
                        DepartmentID = d != null ? d.ID : 0,
                        DepartmentName = d != null ? d.DepartmentName : "N/A",
                        PositionID = p != null ? p.ID : 0,
                        PositionName = p != null ? p.PositionName : "N/A"
                    };
            return q.ToList();
        }

        #region Other DAO Methods (Unchanged)
        public bool Delete(TASK entity)
        {
            try
            {
                var t = DbContext.TASK.First(x => x.ID == entity.ID);
                DbContext.TASK.Remove(t);
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Task deletion failed.", ex);
            }
        }

        public bool GetBack(int id)
        {
            throw new NotSupportedException("Task restoration is not supported.");
        }

        public List<TASKSTATE> GetStates()
        {
            return DbContext.TASKSTATE.OrderBy(s => s.ID).ToList();
        }

        public bool Approve(int taskId, bool isAdmin)
        {
            try
            {
                var t = DbContext.TASK.First(x => x.ID == taskId);
                t.TaskState = isAdmin ? TaskStates.Approved : TaskStates.Delivered;
                t.TaskDeliveryDate = DateTime.Today;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Task approval failed.", ex);
            }
        }
        #endregion
    }
}