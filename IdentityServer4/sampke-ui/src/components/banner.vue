<template>
  <div id="warp-banner">
    <el-link
      :underline="false"
      class="pull-left"
    ><i class="el-icon-s-fold"></i></el-link>
    <el-link
      :underline="false"
      class="pull-right"
    ><i class="el-icon-user"></i></el-link>
  </div>
</template>

<script>
export default {
  name: "banner"
};
</script>

<style lang="scss" scoped>
#warp-banner {
  height: 60px;
  width: 100%;
  line-height: 60px;
  margin: 0px;
  padding: 0px;
  color: rgba(189, 187, 187, 0.582);
}
#warp-banner a i {
  line-height: 60px;
  font-size: 32px;
  font-weight: 1px;
}
</style>

