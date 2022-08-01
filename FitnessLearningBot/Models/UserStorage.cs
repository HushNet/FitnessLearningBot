using System.Collections.Concurrent;
using Telegram.Bot.Types;

namespace FitnessLearningBot.Models;

public class UserStorage : IUserStorage
{
    public ConcurrentDictionary<long, User> users;
    public ConcurrentDictionary<long, User> adminUsers;

    public UserStorage()
    {
        users = new ConcurrentDictionary<long, User>();
        adminUsers = new ConcurrentDictionary<long, User>();
    }
    public User GetUser(Message message)
    {
        var chatId = message.Chat.Id;
        var userId = message.From.Id;
        var userName = message.From.Username;
        
        
        if (users.Any(x => x.Value.userId == userId))
        {
            return users[userId];
        }

        Console.WriteLine("New user created");
        var newUser = CreateUser(message, false);
        return newUser;
    }

    public User CreateUser(Message message, bool IsAdmin)
    {
        var newUser = new User(message.From.Id, message.Chat.Id, message.From.Username, IsAdmin);
        users.TryAdd(message.From.Id, newUser);
        return newUser;
    }
    
    public void DeleteUser(long userId)
    {
        try
        {
            var user = users[userId];
            users.TryRemove(user.userId, out user);
            Console.WriteLine("Пользователь удален");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("Пользователь не найден");
        }
        catch (Exception e)
        {
            Console.WriteLine("Произошла непредвиденная ошибка");
        }
    }

    public User UpdateUser(long userId)
    {
        var user = users[userId];
        var updatedUser = new User(userId, user.chatId, user.UserName, user.IsAdmin);
        
        DeleteUser(userId);
        
        users.TryAdd(userId, updatedUser);
        Console.WriteLine("Пользователь обновлен");
        return updatedUser;

    }
}