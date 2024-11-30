public class CardAnalyser
{
    private readonly _number;

    public CardAnalyser()
    {
        
    }

    public bool IsCardExpired(Card card)
    {
        return card.ExpirationDate < DateTime.Now;
    }
}