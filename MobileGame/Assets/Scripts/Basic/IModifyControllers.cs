namespace MobileGame
{
    internal interface IModifyControllers
    {
        public void Add(IController controller, string name = "");
        public void Delete(IController controller);
    }
}
