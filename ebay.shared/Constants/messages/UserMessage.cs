public static class UserMessages
{
    public const string ERROR = "Lỗi";
    public const string PASSWORD_IS_REQUIRED = "Password is required";
    public const string CONFIRM_PASSWORD_IS_REQUIRED = "Confirm password is required";
    public const string CONFIRM_PASSWORD_DOES_NOT_MATCH_PASSWORD = "Confirm password does not match password";


    public const string PASSWORD_IS_NOT_CORRECT = "Password is not correct";
    public const string LOGIN_SUCCESSFULLY = "Login successfully";
    public const string LOGOUT_SUCCESSFULLY = "Logout successfully";
    public const string REGISTER_SUCCESSFULLY = "Register successfully";
    public const string USERNAME_IS_REQUIRED = "Username is required";
    public const string FULLNAME_IS_REQUIRED = "Fullname is required";
    public const string REFRESH_TOKEN_IS_REQUIRED = "Refresh token is required";
    public const string REFRESH_TOKEN_IS_EXPIRED = "Refresh token is expired";
    public const string REFRESH_TOKEN_SUCCESSFULLY = "Refresh token successfully";


    public const string REFRESH_TOKEN_NOT_FOUND_OR_ALREADY_USED = "Refresh token not found or already used";

    public const string PASSWORD_IS_INVALID = "Password is must be contains 1 uppercase, 1 special character, 1 number, 1 lowercase";
    public const string ROLE_IS_REQUIRED = "Role không được rỗng !";
    public const string USER_ID_IS_INVALID = "Mã người dùng không hợp lệ";

    public const string DOCTOR_ID_IS_INVALID = "Mã bác sĩ không hợp lệ";

    public const string WEIGHT_MUST_BE_A_NUMBER = "Cân nặng phải là số";

    public const string WEIGHT_IS_REQUIRED = "Cân nặng không được rỗng !";


    public const string ROLE_MUST_BE_A_NUMBER = "Role phải là số !";

    public const string ROLE_MUST_BE_A_GUEST = "Lễ tân chỉ có thể thêm được role là bệnh nhân - role 1";

    public const string EMAIL_IS_INVALID = "Email is invaild";

    public const string EMAIL_IS_REQUIRED = "Email is required";

    public const string EMAIL_IS_ALREADY_EXIST = "Email is already existed";

    public const string PHONE_IS_ALREADY_EXIST = "Số điện thoại đã tồn tại !";

    public const string FULL_NAME_IS_REQUIRED = "Họ tên không được rỗng !";
    public const string FULL_NAME_IS_INVALID = "Họ tên không hợp lệ - Phải từ 2 đến 50 ký tự và không chứa số hoặc ký tự đặc biệt !";

    public const string ADDRESS_IS_INVALID = "Địa chỉ không hợp lệ - Phải từ 2 đến 100 ký tự";


    public const string PHONE_IS_REQUIRED = "Số điện thoại không được rỗng !";
    public const string PHONE_IS_INVALID = "Số điện thoại không hợp lệ. Phải bắt đầu bằng 0 và có 10 số";

    public const string IMAGE_IS_INVALID = "Hình ảnh không hợp lệ";

    public const string GENDER_IS_REQUIRED = "Giới tính không được rỗng !";

    public const string GENDER_IS_INVALID = "Giới tính không được rỗng !";

    public const string BIRTH_DATE_IS_REQUIRED = "Năm sinh không được rỗng !";
    public const string GET_DOCTORS_SUCCESSFULLY = "Lấy danh sách bác sĩ thành công";
    public const string GET_RECEPTIONISTS_SUCCESSFULLY = "Lấy danh sách lễ tân thành công";
    public const string GET_USERS_SUCCESSFULLY = "Lấy danh sách người dùng thành công";

    public const string ADD_USER_SUCCESSFULLY = "Thêm người dùng thành công";

    public const string UPLOAD_FILE_SUCCESSFULLY = "Tải hình ảnh lên thành công";
    public const string ROLE_DOCTOR_MUST_HAVE_SPECIALTY = "Bác sĩ phải có chuyên khoa";

    public const string DOCTOR_NOT_FOUND = "Bác sĩ không tồn tại !";


    public const string ROLE_DOCTOR_MUST_HAVE_FULL_INFORMATION = "Bác sĩ phải có đầy đủ thông tin !";

    public const string ADDRESS_IS_REQUIRED = "Địa chỉ không được rỗng !";

    public const string IMAGE_IS_REQUIRED = "Hình ảnh không được rỗng !";

    public const string ROLE_RECRPTIONIST_MUST_HAVE_FULL_INFORMATION = "Lễ tân phải có đầy đủ thông tin !";

    public const string UPDATE_PROFILE_SUCCESSFULLY = "Cập nhật thông tin cá nhân thành công";

    public const string UPDATE_USER_SUCCESSFULLY = "Cập nhật thông tin người dùng thành công";


    public const string GET_PATIENTS_SUCCESSFULLY = "Lấy danh sách bệnh nhân thành công";

    public const string GET_PATIENT_SUCCESSFULLY = "Lấy bệnh nhân thành công";

    public const string PLEASE_USE_ROUTE_GET_PROFILE = "Hãy dùng /get-me để lấy thông tin cá nhân nhé";

    public const string PLEASE_USE_ROUTE_UPDATE_ME = "Hãy dùng /update-me để cập nhật thông tin cá nhân nhé";

    public const string GET_USER_SUCCESSFULLY = "Lấy người dùng thành công";

    public const string GET_USER_PROFILE_SUCCESSFULLY = "Lấy thông tin cá nhân thành công";

    public const string YOU_DO_NOT_HAVE_PERMISSION_TO_FILTER_WITH_THIS_ROLE = "Bạn không có quyền lọc theo các vai trò này !";

    public const string USER_NOT_FOUND = "User not found";
    public const string EMAIL_OR_PASSWORD_IS_INCORRECT = "Email or Password is incorrect";


    public const string CURRENT_USER_NOT_FOUND = "Người dùng hiện tại không tồn tại ";

    public const string GET_APPOINTMENT_PATIENTS_TODAY_SUCCESSFULLY = "Lấy lịch các bệnh nhân hôm nay thành công ";

    public const string ROLE_IS_INVALID = "Role không hợp lệ";

    public const string SPECIALTY_IS_INVALID = "Chuyên khoa không hợp lệ";


    public const string BIRTH_DATE_IS_INVALID = "Ngày sinh không hợp lệ ! Phải là định dạng  Năm-Tháng-Ngày vd 1989-10-20";

    public const string ONLY_ROLE_DOCTOR_HAS_SPECIALTY = "Chỉ bác sĩ mới có chuyên khoa";

    public const string SPECIALTY_MUST_BE_A_NUMBER = "Chuyên khoa phải là số";

    public const string SPECIALTY_IS_REQUIRED = "Chuyên khoa không được rỗng !";
    public const string SPECIALTY_MUST_BE_GREATER_THAN_0 = "Chuyên khoa phải lớn hơn 0";

    public const string NOT_ALLOWED = "Bạn không đủ quyền để xem";
}