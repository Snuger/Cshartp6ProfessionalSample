package com.sampke.kafka.customer


import java.util.{Date, UUID}

case class TestKafka(val id:UUID, val text:String, val updateTime:Date)
