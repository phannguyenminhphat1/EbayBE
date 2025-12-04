public class ValidateStatusChange
{
    public static bool ValidateStatusChangeForRole(string role, int currentStatus, int newStatus)
    {
        // Admin → luôn hợp lệ
        if (role == "Admin") return true;

        return role switch
        {
            "Receptionist" => currentStatus == (int)AppointmentStatus.Scheduled && newStatus == (int)AppointmentStatus.Awaiting
                        || (currentStatus == (int)AppointmentStatus.Scheduled && newStatus == (int)AppointmentStatus.Canceled)
                        || (currentStatus == (int)AppointmentStatus.Awaiting && newStatus == (int)AppointmentStatus.Canceled),

            "Doctor" => (currentStatus == (int)AppointmentStatus.Awaiting && newStatus == (int)AppointmentStatus.Examining)
                        || (currentStatus == (int)AppointmentStatus.Examining && newStatus == (int)AppointmentStatus.Completed),

            "Guest" => currentStatus == (int)AppointmentStatus.Scheduled && newStatus == (int)AppointmentStatus.Canceled,

            "Technician" => currentStatus == (int)MedicalTestStatusEnum.AwaitingTesting && newStatus == (int)MedicalTestStatusEnum.TestingCompleted,

            _ => false
        };
    }
}

