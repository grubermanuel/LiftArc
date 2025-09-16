namespace LiftArc.Models
{
    public class WorkoutPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Exercise> Exercises { get; set; } = new();

        // Temporäre Felder für UI
        public string NewExerciseName { get; set; } = "";
        public int NewExerciseSets { get; set; } = 0;
        public int NewExerciseWeight { get; set; } = 0;
    }

    public class Exercise
    {
        public string Name { get; set; }
        public int Sets { get; set; }
        public int WeightKg { get; set; }
    }
}