using System.Windows.Forms;

namespace StoreOperations_SDK
{
    public enum ItemTypes
    {
        Item = 0,
        Serialized = 1,
        Kit = 3,
        Gasoline = 5,
        Weighted = 6,
        NonInventory = 7,
        Voucher = 9
        //Matrix, Lot Matrix, and Assembly Items don't get an ItemType ID in the database, they're in separate tables
    }
    
    public interface IItemInterface
    {
        int ID { get; }
        ItemTypes ItemType { get; }

        bool Load(int intId);
        bool Save();
        void Search();
        Panel EditDetails();
    }
}