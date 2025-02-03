using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class FixGradeTypeInSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:bus_direction", "from,to")
                .Annotation("Npgsql:Enum:grade_term", "advanced,commission,conditional,first_term,second_term")
                .Annotation("Npgsql:Enum:grade_type", "graded,non_graded")
                .Annotation("Npgsql:Enum:language", "english,polish")
                .Annotation("Npgsql:Enum:lesson_state", "canceled,class,exam")
                .Annotation("Npgsql:Enum:subject_type", "laboratory,lecture,workshop")
                .OldAnnotation("Npgsql:Enum:bus_direction", "from,to")
                .OldAnnotation("Npgsql:Enum:grade_term", "advanced,commission,conditional,first_term,second_term")
                .OldAnnotation("Npgsql:Enum:grade_type", "graded,non_graded")
                .OldAnnotation("Npgsql:Enum:language", "english,polish")
                .OldAnnotation("Npgsql:Enum:lesson_state", "canceled,class,exam")
                .OldAnnotation("Npgsql:Enum:subject_type", "laboratory,lecture,workshop");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:bus_direction", "from,to")
                .Annotation("Npgsql:Enum:grade_term", "advanced,commission,conditional,first_term,second_term")
                .Annotation("Npgsql:Enum:grade_type", "graded,non_graded")
                .Annotation("Npgsql:Enum:language", "english,polish")
                .Annotation("Npgsql:Enum:lesson_state", "canceled,class,exam")
                .Annotation("Npgsql:Enum:subject_type", "laboratory,lecture,workshop")
                .OldAnnotation("Npgsql:Enum:bus_direction", "from,to")
                .OldAnnotation("Npgsql:Enum:grade_term", "advanced,commission,conditional,first_term,second_term")
                .OldAnnotation("Npgsql:Enum:grade_type", "graded,non_graded")
                .OldAnnotation("Npgsql:Enum:language", "english,polish")
                .OldAnnotation("Npgsql:Enum:lesson_state", "canceled,class,exam")
                .OldAnnotation("Npgsql:Enum:subject_type", "laboratory,lecture,workshop");
        }
    }
}
