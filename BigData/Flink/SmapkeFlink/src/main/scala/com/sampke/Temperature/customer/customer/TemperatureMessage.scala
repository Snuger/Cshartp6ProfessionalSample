package com.sampke.Temperature.customer.customer

import java.util.{Date, UUID}

case class TemperatureMessage(val id:UUID, val txt:String, val updateTime:Date)
