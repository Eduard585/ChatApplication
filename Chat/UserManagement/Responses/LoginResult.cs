namespace Chat.UserManagement
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Error { get; set; }
    }
}
