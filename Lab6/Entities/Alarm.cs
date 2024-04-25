namespace Lab6.Entities;

public partial class Alarm
{
    public byte[] Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public bool IsAlarmEnabled { get; set; }

    public DateTime Datetime { get; set; }
}
