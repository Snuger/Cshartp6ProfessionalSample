客户端验证的要点
接口提供方
1:验证服务器的地址
1:API资源接口提供方一定要提供apiresource的名称（options.Audience = "api12"）
2:apiresource一定要有apiscope 至少有一个 ###否则请求会提示invd_scope



调用消费方
1:请求方

