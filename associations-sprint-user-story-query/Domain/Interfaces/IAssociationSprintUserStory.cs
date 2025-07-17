namespace Domain.Interfaces;

public interface IAssociationSprintUserStory
{
    public Guid Id { get; }
    public Guid SprintId { get; }
    public Guid UserStoryId { get; }
    public Guid CollaboratorId { get; }
    public int EffortHours { get; }
    public int CompletionPercentage { get; }
    public void ValidateCompletionPercentage(int completionPercentage);
    public void ValidateEffortHours(int effortHours);
    public void UpdateEffortAndCompletion(int effortHours, int completionPercentage);
}