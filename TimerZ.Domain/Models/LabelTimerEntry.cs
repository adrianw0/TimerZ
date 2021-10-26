namespace TimerZ.Domain.Models
{
    public class LabelTimerEntry
    {
        public int? LabelsId { get; set; }
        public virtual Label Label { get; set; }

        public int? TimerEntriesId { get; set; }
        public virtual TimerEntry TimerEntry { get; set; }
    }
}
