<template>
  <el-container>
    <el-aside width="200px">
      <warp-nav />
    </el-aside>
    <el-container>
      <el-header>
        <warp-banner />
      </el-header>
      <el-main>
        <warp-main />
      </el-main>
    </el-container>
  </el-container>
</template>

<script>
import banner from "@/components/banner";
import maincontent from "@/components/maincontent";
import navigation from "@/components/navigation";

export default {
  name: "index",
  components: {
    "warp-banner": banner,
    "warp-nav": navigation,
    "warp-main": maincontent
  }
};
</script>
<style>
body {
  padding: 0px;
  margin: 0px;
}
.el-header {
  width: 100%;
  background-color: #fff;
  color: #333;
  height: 60px;
  line-height: 60px;
}
.el-aside {
  height: 100vh;
  background-color: #2e2f30d8;
  color: #fff;
  line-height: 60px;
}
.el-main {
  background-color: rgb(243, 242, 242);
}
.pull-left {
  float: left;
}
.pull-right {
  float: right;
}
</style>