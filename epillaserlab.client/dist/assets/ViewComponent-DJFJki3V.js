import{_ as r,d as s,o as p,c as u,a as e}from"./index-tN0-wq2L.js";const d=s({setup(){return{docButtonClick:t=>{let a=t.target.dataset.type;fetch(`/api/document/${a}`,{method:"POST",credentials:"include",headers:{"Content-Type":"application/json"}}).then(async i=>{let l=await i.json();window.open(l.fileUrl,"_blank")})}}}});function c(n,t,a,i,l,b){return p(),u("div",null,[e("button",{type:"button",class:"btn btn-primary",onClick:t[0]||(t[0]=(...o)=>n.docButtonClick&&n.docButtonClick(...o)),"data-type":"returnability"},"Возвращаемость"),e("button",{type:"button",class:"btn btn-primary",onClick:t[1]||(t[1]=(...o)=>n.docButtonClick&&n.docButtonClick(...o)),"data-type":"returnability"},"Продажи по сотрудникам"),e("button",{type:"button",class:"btn btn-primary",onClick:t[2]||(t[2]=(...o)=>n.docButtonClick&&n.docButtonClick(...o)),"data-type":"returnability"},"Продажи по услугам")])}const k=r(d,[["render",c]]);export{k as default};
