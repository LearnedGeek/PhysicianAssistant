using PhysicianAssistant.Models;

namespace PhysicianAssistant.Services;

public interface IContactService
{
    PhoneContact? GetContact(string phoneNumber);
    void SaveContact(PhoneContact contact);
}
