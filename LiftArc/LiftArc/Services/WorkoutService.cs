using LiftArc.Models;

namespace LiftArc.Services
{
    public class WorkoutService
    {
        private List<WorkoutPlan> _allWorkouts = new();

        public Task<List<WorkoutPlan>> GetAllWorkoutsAsync()
        {
            return Task.FromResult(_allWorkouts);
        }

        public Task AddWorkoutAsync(WorkoutPlan plan)
        {
            plan.Id = _allWorkouts.Count > 0 ? _allWorkouts.Max(p => p.Id) + 1 : 1;
            _allWorkouts.Add(plan);
            return Task.CompletedTask;
        }
    }
}