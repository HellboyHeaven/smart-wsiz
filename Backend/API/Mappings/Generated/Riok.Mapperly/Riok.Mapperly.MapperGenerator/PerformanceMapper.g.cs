﻿// <auto-generated />
#nullable enable
namespace API.Mappings
{
    public partial class PerformanceMapper
    {
        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
        public partial global::Core.Models.Attendance Map(global::API.Contracts.Requests.Performance.AttendanceRequest request)
        {
            var target = new global::Core.Models.Attendance();
            target.Id = request.Id;
            target.IsPresent = request.IsPresent;
            return target;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
        public partial global::API.Contracts.Responses.Performance.AttendanceResponse Map(global::Core.Models.Attendance entity)
        {
            var target = new global::API.Contracts.Responses.Performance.AttendanceResponse(
                entity.Id,
                entity.Lesson?.Date ?? throw new System.ArgumentNullException(nameof(entity.Lesson.Date)),
                entity.IsPresent,
                entity.Student != null ? MapToAttendanceStudentResponse(entity.Student) : throw new System.ArgumentNullException(nameof(entity.Student))
            );
            return target;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
        public partial global::System.Collections.Generic.IEnumerable<global::API.Contracts.Responses.Performance.AttendanceResponse> Map(global::System.Collections.Generic.IEnumerable<global::Core.Models.Attendance> entities)
        {
            return global::System.Linq.Enumerable.Select(entities, x => Map(x));
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
        public partial global::Core.Models.Grade Map(global::API.Contracts.Requests.Performance.GradeRequest request)
        {
            var target = new global::Core.Models.Grade();
            target.Id = request.Id;
            target.Term = request.Term;
            target.Value = request.Value;
            return target;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
        public partial global::API.Contracts.Responses.Performance.GradeResponse Map(global::Core.Models.Grade entity)
        {
            var target = new global::API.Contracts.Responses.Performance.GradeResponse(
                entity.Id,
                entity.Student != null ? MapToAttendanceStudentResponse(entity.Student) : throw new System.ArgumentNullException(nameof(entity.Student)),
                entity.Term,
                entity.Value,
                entity.Subject != null ? MapToSubjectResponse(entity.Subject) : throw new System.ArgumentNullException(nameof(entity.Subject))
            );
            return target;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
        public partial global::System.Collections.Generic.IEnumerable<global::API.Contracts.Responses.Performance.GradeResponse> Map(global::System.Collections.Generic.IEnumerable<global::Core.Models.Grade> entities)
        {
            return global::System.Linq.Enumerable.Select(entities, x => Map(x));
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
        private global::API.Contracts.Responses.Performance.AttendanceStudentResponse MapToAttendanceStudentResponse(global::Core.Models.Users.Student source)
        {
            var target = new global::API.Contracts.Responses.Performance.AttendanceStudentResponse(source.Id, source.Firstname, source.Lastname, source.StudentId);
            return target;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
        private global::API.Contracts.Responses.Curriculum.SubjectResponse MapToSubjectResponse(global::Core.Models.Subject source)
        {
            var target = new global::API.Contracts.Responses.Curriculum.SubjectResponse(source.Id, source.Type, source.Name, source.GradeType);
            return target;
        }
    }
}