using UiTests.Helpers;
using UiTests.Managers.Interfaces;
using UiTests.Pages.Interfaces;

namespace UiTests.Managers
{
    public class CharterViewManager(
        ICharterViewPage charterViewPage,
        ISendMessageModal sendMessageModal,
        IMessageSentModal messageSentModal) : ICharterViewManager
    {
        public void MessageCaptain(MessageCaptainData messageCaptainData)
        {
            charterViewPage.ClickMessageCaptain();
            sendMessageModal.ClickCreateNewInquiry();
            sendMessageModal.ClickCalendar();

            sendMessageModal.SelectDay(messageCaptainData.DayInTheMonth!.Value);
            sendMessageModal.SelectGroupSize(messageCaptainData.GroupSize!.Value);
            sendMessageModal.SelectTrip(messageCaptainData.TripOptions!.Value);

            sendMessageModal.TypeMessage(messageCaptainData.Message);
            sendMessageModal.ClickSendMessage();
            Assert.Equal("Message Sent!", messageSentModal.GetMessageSentText());
        }
    }
}
