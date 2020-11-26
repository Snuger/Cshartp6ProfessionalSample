package com.sampke.collections.customer
import org.apache.flink.streaming.api.scala._
import org.apache.flink.api.scala.{createTypeInformation, _}
import org.apache.flink.api.common.functions.{FlatMapFunction, MapFunction}
import org.apache.flink.util.Collector


object CollectionProgram {
  def main(args: Array[String]): Unit = {
    val env=StreamExecutionEnvironment.getExecutionEnvironment
    val words= env.fromElements(WC("LISI", 600),  WC("LISI", 400),  WC("WANGWU", 300),  WC("ZHAOLIU", 700))

//    val wordCounts1 = words.groupBy("word").reduce {
//      (w1, w2) => new WC(w1.word, w1.salary + w2.salary)
//    }
    words.print()

    val groups= words.filter(_.word=="LISI")
    groups.print()

    val groups1=words.filter(item=>item.word=="WANGWU")
    groups1.print()


    var keyWords=env.fromElements("hell,word","hello lilei")

    var text1=keyWords.map(new MapFunction[String,String] {
      override def map(t: String): String = t.toUpperCase()+"--#UperCase"
    })
    text1.print()


    var keyWords_out= keyWords.flatMap(new FlatMapFunction[String, Array[String]] {
      override def flatMap(t: String, collector: Collector[Array[String]]): Unit = {
        var arry = t.toUpperCase().split("\\s+")
        collector.collect(arry)
      }
    })
    keyWords_out.print()

    //var reduceResult=keyWords.reduce()


    var students=env.fromElements(Student("zhangSan",20))
    var dealStudent=students.map(new MapFunction[Student,Student] {
      override def map(t: Student): Student = Student(t.name.toUpperCase(),t.age)
    })
    students.print()



    env.executeAsync("数组的使用")
  }
}
