namespace TimerZ.Domain.Models
{
    public class LabelTimerEntry
    {
        public int LabelsId { get; set; }
        public Label Label { get; set; }

        public int TimerEntriesId { get; set; }
        public TimerEntry TimerEntry { get; set; }
    }
}
