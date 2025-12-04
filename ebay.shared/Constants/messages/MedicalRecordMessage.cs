
public static class MedicalRecordMessages
{
    public const string Error = "Lỗi";

    public const string APPOINTMENT_ID_IS_REQUIRED = "Mã lịch không được rỗng !";
    public const string SYMPTOMS_IS_REQUIRED = "Triệu chứng không được rỗng !";
    public const string DIAGNOSIS_IS_REQUIRED = "Kết luận chẩn đoán không được rỗng !";
    public const string UPDATE_MEDICAL_RECORD_SUCCESSFULLY = "Lưu chẩn đoán thành công";
    public const string UPDATE_AND_COMPLETE_MEDICAL_RECORD_DETAIL_SUCCESSFULLY = "Cập nhật và hoàn thành khám thành công";

    public const string REQUIRES_TEST_IS_REQUIRED = "Chỉ định xét nghiệm không được trống";
    public const string ADD_NEW_SERVICES_IN_MEDICAL_RECORD_DETAIL_SUCCESSFULLY = "Thêm mới chỉ định xét nghiệm thành công";


    public const string CANNOT_UPDATE_MEDICAL_RECORD_FOR_THIS_APPOINTMENT = "Không thể cập nhật hồ sơ bệnh án này vì trạng thái của lịch hẹn không phải là đang khám hoặc hoàn thành";

    public const string LENGTH_OF_SYMSTOMS = "Độ dài triệu chứng phải từ 5 ký tự";
    public const string LENGTH_OF_DIAGNOSIS = "Độ dài kết luận chẩn đoán phải từ 5 ký tự";
    public const string NOTE = "Ghi chú không được rỗng !";
    public const string DIAGNOSE_SUCCESSFULLY = "Chẩn đoán thành công";
    public const string APPOINTMENT_ID_NOT_FOUND = "Không tìm thấy lịch hẹn";
    public const string MEDICAL_RECORD_NOT_FOUND = "Không tìm thấy hồ sơ bệnh án !";
    public const string MEDICAL_RECORD_DETAIL_NOT_FOUND = "Không tìm thấy chi tiết hồ sơ bệnh án !";
    public const string MEDICAL_RECORD_DETAIL_MUST_BE_A_NUMBER = "Mã chi tiết hồ sơ bệnh án phải là số !";


    public const string SERVICE_NOT_FOUND = "Không tìm thấy dịch vụ !";
    public const string RESULT_IS_REQUIRED = "Kết quả xét nghiệm không được trống !";
    public const string MEDICAL_RECORD_IS_NOT_REQUIRES_TEST = "Hồ sơ bệnh án này không được chỉ định xét nghiệm !";

    public const string MEDICAL_RECORD_ID_IS_REQUIRED = "Mã hồ sơ bệnh án không được trống !";
    public const string MEDICAL_RECORD_DETAIL_ID_IS_REQUIRED = "Mã chi tiết hồ sơ bệnh án không được trống !";

    public const string SERVICE_ID_IS_REQUIRED = "Dịch vụ không được trống !";
    public const string MEDICAL_RECORD_ID_MUST_BE_A_NUMBER = "Mã hồ sơ bệnh án phải là số";
    public const string SERVICE_ID_MUST_BE_A_NUMBER = "Mã dịch vụ phải là số !";
    public const string ASSIGN_MEDICAL_TESTS_SUCCESSFULLY = "Chỉ định xét nghiệm thành công";
    public const string MEDICAL_TEST_SERVICE_IS_ALREADY_EXIST = "Đã chỉ định xét nghiệm này cho bệnh án này rồi";

    public const string MEDICAL_TEST_ID_MUST_BE_A_NUMBER = "Mã xét nghiệm phải là số !";

    public const string MEDICAL_TEST_NOT_FOUND = "Không tìm thấy chỉ định xét nghiệm !";

    public const string UPDATE_MEDICAL_TEST_SUCCESSFULLY = "Cập nhật xét nghiệm thành công";

    public const string RESULT_IS_REQUIRED_WHEN_COMPLETED = "Kết quả không được trống khi xét nghiệm hoàn thành ! Hãy điền kết quả xét nghiệm.";

    public const string DIAGNOSIS_IS_REQUIRED_WHEN_COMPLETED = "Kết quả chấn đoán không được trống khi nhấn hoàn thành khám bệnh !";

    public const string STATUS_IS_COMPLETED_SO_CANNOT_UPDATE_TO_ANOTHER_STATUS = "Trạng thái đã hoàn thành, không thể cập nhật sang trạng thái khác";

    public const string DIAGNOSIS_IS_REQUIRED_WHEN_NO_TEST = "Phải có kết luận nếu không chỉ định xét nghiệm !";

    public const string CAN_NOT_UPDATE_RESULT_TEST_BEFORE_ITS_COMPLETE = "Không thể cập nhật kết quả khi xét nghiệm chưa hoàn thành.";

    public const string GET_ALL_MEDICAL_RECORD_DETAIL_SUMMARY_SUCCESSFULLY = "Lấy danh sách lịch sử khám thành công";
    public const string GET_MEDICAL_RECORD_DETAIL_SUCCESSFULLY = "Lấy chi tiết khám thành công";
    public const string GET_ALL_MEDICAL_RECORD_DETAIL_SUCCESSFULLY = "Lấy danh sách chi tiết khám thành công";

    public const string GET_MEDICAL_RECORD_SUCCESSFULLY = "Lấy hồ sơ khám thành công";

    public const string THIS_APPOINTMENT_DO_NOT_HAVE_MEDICAL_RECORD_DETAIL = "Lịch hẹn này chưa thực hiện khám";






}
