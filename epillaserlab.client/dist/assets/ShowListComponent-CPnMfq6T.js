import{d as $,u as L,b as O,r as _,_ as j,g as f,o as x,c as N,h as u,p as r,y as h,e as g,t as I}from"./index-1Nx-P1XQ.js";import{I as K,O as P,D as U}from"./style-DmTOC_lk.js";import{U as V}from"./UserStore-BqrtbZif.js";const F=$({components:{TableBody:K,TableHead:P,DataTable:U},setup(){const e=L(),i=O(),y=window.location.hostname,m=_([]),o=_({}),s=_({}),c=t=>t.target.parentNode.dataset.id,v=async t=>{let a=c(t);await e.push({name:"dashboard.masters.edit",params:{id:a}})},p=async t=>{if(!confirm("Вы точно хотите удалить запись?"))return;let d=c(t);await fetch(`/api/masters/${d}`,{method:"DELETE",credentials:"include"}).then(async n=>{const l=await n.json();l.message=="OK"?(i.success("Запись удалена"),b(void 0)):l.message=="BLOCK"?i.error("Удаление записи приведет к потере данных"):i.error("Ошибка при удалении записи")}).catch(n=>e.push({name:"dashboard"}))},b=async t=>{let a,d,n,l;n=s.value.sort,typeof t=="string"?(a=o.value.page,d=o.value.limit,n=n=="asc"?"desc":"asc",l=t.split(":")[0],l!==s.value.order&&(n="desc")):typeof t==typeof{}?(a=t.page-1<0?0:t.page-1,d=t.per_page,l=s.value.order):(a=o.value.page,d=o.value.limit,l=s.value.order);let D={page:a,limit:d,sort:n,order:l,branchId:null};var k=await V.user();k.roleId==2&&(D.branchId=k.admin.branchId);var S=new URLSearchParams(D);await fetch(`/api/masters?${S}`,{headers:{"Content-Type":"application/json"},credentials:"include"}).then(async w=>{let B=await w.json();if(B.message="OK"){let C=B.data,T=C.max;a=C.page;let E=C.recs;m.value=E,o.value={...o.value,page:a,total:T,limit:d},s.value={...s.value,sort:n,order:l}}else m.value=[],o.value={...o.value,page:0,total:0,limit:d},s.value={...s.value,sort:n,order:l}}).catch(w=>e.push({name:"dashboard"}))};return{tableData:m,pagination:o,loadData:b,sort:s,delClick:p,editClick:v,hostname:y}}}),R=F,z=["src"],H=g("TableBodyCell",{class:"d-flex justify-content-center",colspan:"2"}," Нет записей ",-1);function J(e,i,y,m,o,s){const c=f("table-head"),v=f("router-link"),p=f("table-body"),b=f("data-table");return x(),N("div",null,[u(b,{rows:e.tableData,pagination:e.pagination,sort:e.sort,striped:"",onLoadData:e.loadData,style:{"font-size":"large !important"}},{thead:r(()=>[u(c,null,{default:r(()=>[h("Изображение")]),_:1}),u(c,{sortable:"FIO",onSorting:e.loadData},{default:r(()=>[h("ФИО")]),_:1},8,["onSorting"]),u(c,{sortable:"branch",onSorting:e.loadData},{default:r(()=>[h("Филиал")]),_:1},8,["onSorting"]),u(c,null,{default:r(()=>[u(v,{to:{name:"dashboard.masters.add"},tag:"button",type:"button",class:"btn btn-success"},{default:r(()=>[h("Добавить")]),_:1})]),_:1})]),tbody:r(({row:t})=>[u(p,null,{default:r(()=>[g("img",{src:`/resources/images/${t.photoPath}`,width:"150px",height:"150px"},null,8,z)]),_:2},1024),u(p,{textContent:I(t.fio)},null,8,["textContent"]),u(p,{textContent:I(t.address)},null,8,["textContent"]),u(p,{"data-id":t.masterId},{default:r(()=>[g("button",{type:"button",class:"btn btn-primary me-2",onClick:i[0]||(i[0]=(...a)=>e.editClick&&e.editClick(...a))},"ИЗМЕНИТЬ"),g("button",{type:"button",class:"btn btn-danger",onClick:i[1]||(i[1]=(...a)=>e.delClick&&e.delClick(...a))},"УДАЛИТЬ")]),_:2},1032,["data-id"])]),empty:r(()=>[H]),_:1},8,["rows","pagination","sort","onLoadData"])])}const Q=j(R,[["render",J]]);export{Q as default};