#!/usr/bin/python

import psycopg2
conn = psycopg2.connect(database="medi_oa", user="medi_oa",
                        password="medi_oa@123", host="172.19.32.152", port="5432")
print("Opened database successfully")
cur = conn.cursor()
cur.execute("SELECT tianbaoryid as renyuanid,tianbaoryxm as renyuanxm,sum(gongshisl) as shiwugs from  cmp_wim_01.wim_shiwugs_0001  where zuofeibz =0  group by tianbaoryid ,tianbaoryxm")
rows = cur.fetchall()
for row in rows:
    print(row)
print("Operation done successfully")
conn.close()
