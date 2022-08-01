namespace FitnessLearningBot.Models;

public class User
{
    public long userId { get; set; }
    public long chatId { get; set; }
    public string UserName { get; set; }
    public bool IsAdmin { get; set; }

    public User(long userId, long chatId, string userName, bool isAdmin)
    {
        this.userId = userId;
        this.chatId = chatId;
        UserName = userName;
        IsAdmin = isAdmin;
    }
}