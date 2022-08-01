using Telegram.Bot.Types;

namespace FitnessLearningBot.Models;

public interface IUserStorage
{
    public User GetUser(Message message);
    public User CreateUser(long userId, long chatId, string userName);
    public void DeleteUser(long userId);
    public User UpdateUser(long userId);
}