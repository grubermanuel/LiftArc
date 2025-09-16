using LiftArc.Models;

namespace LiftArc.Services
{
    public class WorkoutService
    {
        // Dummy-Daten
        private List<WorkoutPlan> _allWorkouts = new List<WorkoutPlan>
        {
            new WorkoutPlan { Id = 1, Name = "Push & Pull", Description = "Oberkörper Training" },
            new WorkoutPlan { Id = 2, Name = "Beine & Core", Description = "Unterkörper Training" }
        };

        public Task<List<WorkoutPlan>> GetAllWorkoutsAsync()
        {
            return Task.FromResult(_allWorkouts);
        }

        public Task AddWorkoutAsync(WorkoutPlan workout)
        {
            workout.Id = _allWorkouts.Max(w => w.Id) + 1;
            _allWorkouts.Add(workout);
            return Task.CompletedTask;
        }
    }
}