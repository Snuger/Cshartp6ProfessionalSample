namespace ConsoleTypesetting
{
    public class T1
    {
        public static int publicInt;
        internal static int internalInt;
        private static int privateInt = 0;

        static T1()
        {
            M1.publictInt = 10;
            M1.internalInt = 12;
        }


        public class M1
        {
            public static int publictInt;
            protected static int protectedInit;
            internal static int internalInt;

            private static int privateInt;

        }

    }


}