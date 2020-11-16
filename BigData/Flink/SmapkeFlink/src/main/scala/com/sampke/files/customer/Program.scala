package com.sampke.files.customer
import org.apache.flink.streaming.api.scala.{StreamExecutionEnvironment, _}

object Program {
  def main(args: Array[String]): Unit = {

      val env=StreamExecutionEnvironment.getExecutionEnvironment

    //2.准备数据
    val text = env.fromElements(
      "To be, or not to be,that is the question",
      "Whether 'tis nobler in the mind to suffer",
      "The slings and arrows of outrageous fortune",
      "Or to take arms against a sea of troubles,")

    val counts = text.flatMap(_.toLowerCase.split("\\W+")).map((_, 1)).keyBy(0).sum(1)

    //输出文本
    text.print()

    //4.将结果打印出来
    counts.print()

    env.execute("Flink Streaming Wordcount")

  }
}
