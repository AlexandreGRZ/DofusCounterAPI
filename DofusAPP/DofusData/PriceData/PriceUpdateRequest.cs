namespace DofusData.PriceData;

public class PriceUpdateRequest
{
    public int AnkamaId { get; set; } // ID de l'objet Dofus
    public int Value { get; set; } // Prix en kamas
}