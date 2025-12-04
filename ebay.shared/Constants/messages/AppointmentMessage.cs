public static class AppointmentMessages
{
    public const string ERROR = "Lỗi";
    public const string MAKE_APPOINTMENT_SUCCESSFULLY = "Đặt lịch khám thành công";

    public const string DOCTOR_ID_IS_REQUIRED = "Mã bác sĩ không được rỗng !";

    public const string ENTRY_SERVICE_NOT_FOUND = "Không tìm thấy dịch vụ khám đầu vào !";

    public const string DOCTOR_ID_IS_INVALID = "Mã bác sĩ không hợp lệ !";

    public const string MUST_HAVE_DOCTOR_ID_AND_DATE_AND_TIME_TOGETHER_WHEN_UPDATING_SCHEDULED = "Phải có cả mã bác sĩ, ngày hẹn, thời gian khi muốn cập nhật lịch";

    public const string SELECTED_TIME_SLOT_IS_NOT_AVAILABLE = "Thời gian bạn chọn hiện tại không còn trống !";

    public const string DOCTOR_NOT_FOUND = "Không tìm thấy bác sĩ !";

    public const string USER_NOT_FOUND = "Không tìm thấy người dùng";

    public const string PHONE_IS_ALREADY_EXISTED = "Số điện thoại đã tồn tại - vui lòng chọn 'Đã từng khám' ";


    public const string APPOINTMENT_ID_IS_INVALID = "Mã lịch không hợp lệ !";

    public const string UPDATE_APPOINTMENT_SUCCESSFULLY = "Cập nhật lịch thành công !";
    public const string CHECK_IN_SUCCESSFULLY = "Check-In thành công";


    public const string MUST_HAVE_BOTH_DATE_AND_TIME = "Phải truyền cả ngày và giờ nếu muốn cập nhật";

    public const string APPOINTMENT_MUST_BE_A_NUMBER = "Mã lịch phải là số !";

    public const string STATUS_MUST_BE_A_NUMBER = "Trạng thái phải là số !";

    public const string STATUS_IS_REQUIRED = "Trạng thái không được trống !";
    public const string STATUS_ID_IS_INVALID = "Trạng thái không hợp lệ ";

    public const string UPDATE_STATUS_ID_IS_INVALID = "Cập nhật trạng thái không hợp lệ ";

    public const string UPDATE_STATUS_ID_IS_INVALID_ONLY_CAN_COMPLETE = "Cập nhật trạng thái không hợp lệ - Trạng thái đang là đang khám thì chỉ có thể cập nhật thành hoàn thành ";



    public const string APPOINTMENT_MUST_BE_POSITIVE = "Mã lịch không được âm !";

    public const string APPOINTMENT_NOT_FOUND = "Không tìm thấy lịch !";

    public const string NOTE_IS_REQUIRED = "Ghi chú không được rỗng !";
    public const string NOTE_IS_INVALID = "Ghi chú phải từ 5 đến 50 100 ký tự";



    public const string CANNOT_UPDATE_A_COMPLETED_OR_CANCELED_APPOINTMENT = "Không thể cập nhật lịch có trạng thái đã hoàn thành hoặc đã hủy !";



    public const string ALREADY_HAVE_AN_APPOINTMENT = "Bạn đã có lịch khám trong thời gian này. Vui lòng chọn giờ khác !";
    public const string SCHEDULED_DATE_IS_INVALID = "Ngày hẹn khám không hợp lệ. Không được nhỏ hơn ngày hiện tại. Vui lòng chọn lại !";
    public const string NO_DOCTORS_AVAILABLE_AT_THIS_TIME = "Không có bác sĩ nào trống vào thời điểm này.Vui lòng chọn giờ khác ! ";
    public const string YOUR_ROLE_IS_INVALID_TO_DO_THIS_FUNCTION = "Vai trò của bạn không thực hiện được chức năng này ! ";
    public const string GET_AVAILABLE_TIMES = "Lấy giờ trống thành công";
    public const string GET_APPOINTMENTS_SUCCESSFULLY = "Lấy danh sách lịch hẹn thành công";
    public const string GET_APPOINTMENT_SUCCESSFULLY = "Lấy lịch hẹn thành công";
    public const string DATE_IS_REQUIRED = "Ngày hẹn không được rỗng !";
    public const string DATE_IS_INVALID = "Ngày hẹn không hợp lệ - Định dạng yyyy-mm-dd";
    public const string FILTER_DATE_IS_INVALID = "Ngày không hợp lệ - Định dạng yyyy-mm-dd";


    public const string START_DAY_AND_END_DAY_IS_REQUIRED = "Nếu có ngày bắt đầu phải có ngày kết thúc và ngược lại !";


    public const string START_DATE_IS_INVALID = "Ngày bắt đầu không hợp lệ - Định dạng yyyy/mm/dd";

    public const string START_DATE_MUST_BE_LESS_THAN_END_DAY = "Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc";
    public const string END_DATE_IS_INVALID = "Ngày kết thúc không hợp lệ - Định dạng yyyy/mm/dd";

    public const string TIME_IS_REQUIRED = "Giờ hẹn không được rỗng !";
    public const string TIME_IS_INVALID = "Giờ hẹn không hợp lệ - Định dạng hh:mm:00";

    public const string TIME_SLOT_NOT_FOUND = "Không tìm thấy giờ hẹn phù hợp !";

    public const string TIME_SLOT_IS_NOT_AVAILABLE = "Thời gian này không còn trống, vui lòng chọn thời gian khác";

    public const string APPOINTMENT_NOT_EXAMINING = "Trạng thái phải là đang khám hoặc đang chờ xét nghiệm hoặc đã xét nghiệm xong mới thực hiện chẩn đoán được ";
    public const string SCHEDULED_STATUS = "Đã đặt lịch";
    public const string AWAITING_STATUS = "Đang chờ khám";
    public const string EXAMINING_STATUS = "Đang khám";
    public const string COMPLETED_STATUS = "Đã khám xong";
    public const string CANCEL_STATUS = "Đã hủy";
    public const string AWAITING_TESTING_STATUS = "Đang chờ xét nghiệm";
    public const string ADD_PRESCRIPTION = "Đã kê đơn thuốc";

    public const string TESTING_COMPLETED_STATUS = "Đã xét nghiệm xong";



}
