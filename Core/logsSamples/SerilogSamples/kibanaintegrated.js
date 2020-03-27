
function getKibanaCallUrls(name,soft){
    var _soft=new Array();
    if(soft.length>0){
        var _num=0;
        for (var t=0;t<soft.length;t++){
            if(soft[t]!=""){
                _soft[_num]=soft[t];
                _num+=1;
            }
        }
    }
    var url="/kibana/app/kibana#/dashboard/28cbc0f0-6365-11ea-954b-4b21879036fa?";
    return url+create_Parms_g(name,_soft)+"&"+create_Parms_a(name);
}

function create_Parms_g(name,soft){
    var g_refreshInterval="refreshInterval:(pause:!t,value:0),time:(from:now%2Fd,to:now%2Fd))";
    if(soft.length===0){
        return "_g=("+g_refreshInterval;
    }else{
        var softsplit=soft.join(",");
        var softsplit2=softsplit.replace(",",",%20");
        var g_meta=",meta:(alias:!n,controlledBy:'1583908995590',disabled:!f,index:e3a3c040-514e-11ea-84df-c57a3a8513ec,key:fields.ApplicationName.keyword,negate:!f,params:!("+softsplit+"),type:phrases,value:'"+softsplit2+"')";
        var g_query=",query:(bool:(minimum_should_match:1,should:!("+create_Parms_g_match_phrase(soft)+")))))";
        return "_g=(filters:!(('$state':(store:globalState)"+g_meta+g_query+","+g_refreshInterval;
    }
}

function create_Parms_g_match_phrase(soft){
    var  match_phrase=new Array();
    for(var i=0;i<soft.length;i++){
         match_phrase[i]="(match_phrase:(fields.ApplicationName.keyword:"+soft[i]+"))";
    }
    return match_phrase.join(",");
}

function create_Parms_a(name){
    return "_a=(description:'',filters:!(),fullScreenMode:!f,options:(hidePanelTitles:!t,useMargins:!f),panels:!((embeddableConfig:(),gridData:(h:24,i:'4478a3ed-2e0c-4274-bbbc-7d231bc556a9',w:14,x:0,y:0),id:ff7289b0-6363-11ea-954b-4b21879036fa,panelIndex:'4478a3ed-2e0c-4274-bbbc-7d231bc556a9',type:visualization,version:'7.4.0'),(embeddableConfig:(),gridData:(h:24,i:'9f1ef2ab-b911-401d-8d4d-30c437743ccc',w:33,x:14,y:0),id:ddc9c390-61d4-11ea-93a7-e3639f9adefa,panelIndex:'9f1ef2ab-b911-401d-8d4d-30c437743ccc',type:search,version:'7.4.0')),query:(language:kuery,query:''),timeRestore:!f,title:"+name+",viewMode:view)";
}

