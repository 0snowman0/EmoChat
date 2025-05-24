namespace Analys.api.contracts.AnalysUtilies
{
    public interface IAnalysisProcessor
    {
        Task<List<int>> Get_Top_UserEmojiUsage(string emoji , int top);
        Task<List<string>> Get_Top_Emoji_For_User(int user_id , int top);

        Task<List<string>> Get_SecondPage_Top_Emoji_For_User(int user_id, int pageSize);
        Task<List<int>> Get_SecondPage_Top_UserEmojiUsage(string emoji, int pageSize);
    }
}
