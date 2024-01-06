//イテレーターを使用する理由（foreachだけではダメなのか）

//1.柔軟性とカスタマイズ性:
//IEnumeratorを実装することで、独自の反復処理を実装できます。カスタムな反復処理が可能になります。
//foreachは基本的な反復構文ですが、特定のケースに対応できない場合、カスタムな反復処理が必要になります。

//2.異なるデータ構造への適用:
//IEnumeratorを利用すると、配列やリストだけでなく、異なる種類のデータ構造（木構造、グラフ、カスタムコレクションなど）に
//対しても反復処理を行えます。foreachは特定の型に対してのみ動作しますが、IEnumeratorは柔軟に適用できます。

//3.外部イテレータの制御:
//IEnumeratorを使用すると、反復処理の進行状況を制御できます。MoveNext()やReset()などのメソッドを使って、イテレータを制御できます。
//これにより、途中で反復処理を停止したり、リセットしたりすることができます。

//4.非同期操作のサポート:
//IEnumeratorやIEnumerableを使うことで、非同期操作をサポートすることができます。コレクションの非同期的な要素の処理に役立ちます。




//①イテレーターの実装
//以下、出力内容

//Straight traversal:
//First
//Second
//Third

//Reverse traversal:
//Third
//Second
//First

using System;
using System.Collections;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Iterator.Conceptual
{
    abstract class Iterator : IEnumerator
    {
        object IEnumerator.Current => Current();

        public abstract int Key();

        public abstract object Current();

        public abstract bool MoveNext();

        public abstract void Reset();
    }

    abstract class IteratorAggregate : IEnumerable
    {
        public abstract IEnumerator GetEnumerator();
    }

    // 初期化時に与えられたreverseフラグに基づいて、コレクションを昇順または降順のアルファベット順に反復します。
    class AlphabeticalOrderIterator : Iterator
    {
        private WordsCollection _collection;

        private int _position = -1;

        private bool _reverse = false;

        public AlphabeticalOrderIterator(WordsCollection collection, bool reverse = false)
        {
            this._collection = collection;
            this._reverse = reverse;

            if (reverse)
            {
                this._position = collection.getItems().Count;
            }
        }

        // 現在の要素を返します。
        public override object Current()
        {
            return this._collection.getItems()[_position];
        }

        // 現在の位置（キー）を返します。
        public override int Key()
        {
            return this._position;
        }

        // イテレータを次の要素に移動させます。
        // 次の要素があれば true、ない場合は false を返します。
        public override bool MoveNext()
        {
            int updatedPosition = this._position + (this._reverse ? -1 : 1);

            if (updatedPosition >= 0 && updatedPosition < this._collection.getItems().Count)
            {
                this._position = updatedPosition;
                return true;
            }
            else
            {
                return false;
            }
        }

        // イテレータを最初の要素の前に戻します。通常、反復処理を最初からやり直すために使用されます。
        public override void Reset()
        {
            this._position = this._reverse ? this._collection.getItems().Count - 1 : 0;
        }
    }

    // 文字列のリストが含まれている
    class WordsCollection : IteratorAggregate
    {
        List<string> _collection = new List<string>();

        bool _direction = false;

        // イテレーションの方向を逆に切り替えます（昇順または降順）。
        public void ReverseDirection()
        {
            _direction = !_direction;
        }

        // コレクション内のすべてのアイテムを取得します。
        public List<string> getItems()
        {
            return _collection;
        }

        // コレクションにアイテムを追加します。
        public void AddItem(string item)
        {
            this._collection.Add(item);
        }

        // リスト要素を反復処理するためのイテレータを取得する
        public override IEnumerator GetEnumerator()
        {
            return new AlphabeticalOrderIterator(this, _direction);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var collection = new WordsCollection();
            collection.AddItem("First");
            collection.AddItem("Second");
            collection.AddItem("Third");

            Console.WriteLine("Straight traversal:");

            foreach (var element in collection)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine("\nReverse traversal:");

            collection.ReverseDirection();

            foreach (var element in collection)
            {
                Console.WriteLine(element);
            }
        }
    }
}





//②非同期iterator
//コレクションに追加になった後に、次々処理していく
//add:0 process:0 add:1 process:1・・・を繰り返し出力する
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//class Program
//{
//    static async Task Main(string[] args)
//    {
//        var collection = new List<int>();

//        Task producingTask = ProduceDataAsync(collection);
//        Task processingTask = ProcessCollectionAsync(collection);

//        await Task.WhenAll(producingTask, processingTask);
//    }

//    static async Task ProduceDataAsync(List<int> collection)
//    {
//        for (int i = 0; i < 5; i++)
//        {
//            await Task.Delay(1000);
//            collection.Add(i);
//            Console.WriteLine($"Added: {i}");
//        }
//    }

//    static async Task ProcessCollectionAsync(List<int> collection)
//    {
//        while (true)
//        {
//            // コレクションが空でない場合は処理を行う
//            if (collection.Count > 0)
//            {
//                int item = collection[0];
//                collection.RemoveAt(0);
//                Console.WriteLine($"Processing: {item}");
//                await Task.Delay(500);
//            }
//            else
//            {
//                // コレクションが空になったら待機する
//                await Task.Delay(200);
//            }
//        }
//    }
//}

