namespace ConsoleTypesetting
{
    public class A
    {
        public A(int x, out int y)
        {
            y = x;
        }
    }

    public class B : A
    {
        public B(int bx) : base(bx, out var by)
        {

        }

    }
}