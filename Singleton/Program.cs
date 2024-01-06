//シングルトンパターンの目的・・・
//特定のクラスがアプリケーション全体で1つだけのインスタンスを持つことを保証することです。
//つまり、そのクラスのインスタンスが1つしか存在しないことを保証することが重要です。
//これにより、複数の箇所から同じインスタンスにアクセスでき、データの共有や状態の一貫性を保つことができます。
//例えば、設定情報やデータベースへの接続など、アプリケーション全体で共有されるべきものにシングルトンを使用することがあります。


//スレッドセーフについて・・・
//スレッドセーフなシングルトンを実装することで、複数のスレッドから安全にアクセスできるようになります。
//特にマルチスレッド環境では、同時に複数のスレッドからインスタンスにアクセスされる可能性があるため、
//それらの競合を防ぐためにスレッドセーフな手法が必要です。


//スレッドセーフ（Lazy）について・・・
//Lazy<T> は、スレッドセーフな遅延初期化を実現するための仕組みの一つです。Lazy<T>を利用することで、
//複数のスレッドから同時にアクセスされた際にも、1つのインスタンスが安全に初期化され、その後再利用されることが保証されます。
//Lazy<T>を使うことで、最初のアクセス時に初期化が行われ、その後のアクセスでは初期化済みのインスタンスが返されるため、
//スレッドセーフなシングルトンを実装する際に便利です。これにより、複数のスレッドが同時にGetInstanceメソッドを呼び出しても、
//初期化が競合することなく、1つのインスタンスが返されることが保証されます。


//インスタンスの上書きについて・・・（インスタンスはLazyでは最初のインスタンスを返し続けるので、上書きされないはず。。。）
//GetInstanceメソッドは、初めて呼び出されたときにインスタンスを作成し、以降はその作成済みのインスタンスを返すように実装されます。
//GetInstanceでインスタンスの作成と取得を行うのが一般的で、作成と取得をそれぞれのメソッドに分解する事は通常しない。

//インスタンスの唯一性
//シングルトンクラスのインスタンスの特定のプロパティやフィールドが変更されると、
//同じインスタンスを使用している他の場所でその変更が見えるようになります。
//つまり、マルチスレッドでも、アプリケーション全体で１つのインスタンスを利用している事になる。


//まとめ・・・
//Lazyを使用して、最初の初期化されたインスタンスが、次回以降も返される。（アプリケーション全体で１つのインスタンス）
//通常別のインスタンスを作成する事はシングルトンパターンの意図に反する
//よって、マルチスレッドな環境下でシングルトンを実装する時は、Lazyを利用する事
//Lazyではなく、lockステートメントを使うパターンもある




// ①FOO FOO または、BAR BARとなる。スレッドセーフなので、OK
//マルチスレッド(Lazyなし、lockあり)
// FOO BARという風にはならないので、スレッド競合で予期せぬ値が入らないので良い

using System;

namespace SingletonThreadSafe
{
    public class Singleton
    {
        private static Singleton _instance;
        private static readonly object _lock = new object();

        public static Singleton GetInstance(string value)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                        _instance.Value = value;
                    }
                }
            }
            return _instance;
        }

        private Singleton() { }

        public string Value { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
                "{0}\n{1}\n\n{2}\n",
                "If you see the same value, then singleton was reused (yay!)",
                "If you see different values, then 2 singletons were created (booo!!)",
                "RESULT:"
            );

            //スケジューリングやプロセッサの実行状況によって、どちらのスレッドが先に実行されるかは保証されません。
            Thread process1 = new Thread(() =>
            {
                TestSingleton("FOO");
            });
            Thread process2 = new Thread(() =>
            {
                TestSingleton("BAR");
            });

            process1.Start();
            process2.Start();

            process1.Join();
            process2.Join();
        }

        public static void TestSingleton(string value)
        {
            Singleton singleton = Singleton.GetInstance(value);
            Console.WriteLine(singleton.Value);
        }
    }
}



//② マルチスレッド＋Lazy(lockなし)
//これでも、FOO BAR / FOO FOO / BAR BARだったりするので、ダメ

//スレッドセーフなSingletonの実装では、Lazy<T>を使用することで初期化をスレッドセーフに行いますが、
//TestSingletonメソッド内でシングルトンのValueを設定しているため、複数のスレッドが同時にValueを設定することがあります。

//スレッド間で競合状態が発生し、期待した値が出力されないことがあります。正確な順序や特定の値が出力されることは、
//競合状態のため保証されません。

//シングルトンパターンはインスタンスを1つだけ保持することを目的としていますが、スレッド間でのデータ競合や同時アクセスが発生すると、
//意図しない値が設定される可能性があります。

//このような問題を回避するためには、スレッドセーフな方法で値を設定する必要があります。
//たとえば、lockステートメントを使用して値を設定する部分を排他制御することで、競合を防ぐことができます。

//using System;
//using System.Threading.Tasks;

//namespace SingletonThreadSafe
//{
//    public class Singleton
//    {
//        private static readonly Lazy<Singleton> lazyInstance = new Lazy<Singleton>(() => new Singleton());

//        public static Singleton Instance { get { return lazyInstance.Value; } }

//        private Singleton() { }

//        public string Value { get; set; }
//    }

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine(
//                "{0}\n{1}\n\n{2}\n",
//                "If you see the same value, then singleton was reused (yay!)",
//                "If you see different values, then 2 singletons were created (booo!!)",
//                "RESULT:"
//            );

//            Task[] tasks = new Task[2];
//            tasks[0] = Task.Run(() => TestSingleton("FOO"));
//            tasks[1] = Task.Run(() => TestSingleton("BAR"));

//            Task.WaitAll(tasks);
//        }

//        public static void TestSingleton(string value)
//        {
//            Singleton singleton = Singleton.Instance;
//            singleton.Value = value;
//            Console.WriteLine(singleton.Value);
//        }
//    }
//}




// ③ FOO BAR hoge hoge2の順番に毎回なる。
// シングルスレッド＋Lazy(lockなし)
// シングルスレッドなので当たり前。あまり意味ない。

//using System;

//namespace SingletonThreadSafe
//{
//    public class Singleton
//    {
//        private static readonly Lazy<Singleton> lazyInstance = new Lazy<Singleton>(() => new Singleton());

//        public static Singleton Instance { get { return lazyInstance.Value; } }

//        private Singleton() { }

//        public string Value { get; set; }
//    }

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine(
//                "{0}\n{1}\n\n{2}\n",
//                "If you see the same value, then singleton was reused (yay!)",
//                "If you see different values, then 2 singletons were created (booo!!)",
//                "RESULT:"
//            );

//            TestSingleton("FOO");
//            TestSingleton("BAR");

//            TestSingleton("hoge");
//            TestSingleton("hoge2");
//        }

//        public static void TestSingleton(string value)
//        {
//            Singleton singleton = Singleton.Instance;
//            singleton.Value = value;
//            Console.WriteLine(singleton.Value);
//        }
//    }
//}
