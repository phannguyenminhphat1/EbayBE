public class CheckAndUpdateString
{
    public static void CheckAndUpdateValueString(
            Dictionary<string, string> errors,
            string fieldKey,            // key lỗi (vd: "fullname", "email", "address")
            string? newValue,           // giá trị mới từ DTO
            string currentValue,        // giá trị hiện tại trong DB
            string errorMessage,        // message nếu rỗng
            Action<string> updateAction // hành động cập nhật nếu hợp lệ
        )
    {
        if (newValue != null) // chỉ check khi có truyền vào
        {
            var trimmedValue = newValue.Trim();

            if (string.IsNullOrEmpty(trimmedValue))
            {
                errors[fieldKey] = errorMessage;
            }
            else if (trimmedValue != currentValue)
            {
                updateAction(trimmedValue);
            }
        }
    }
}