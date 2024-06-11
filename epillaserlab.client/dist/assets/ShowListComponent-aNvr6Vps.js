import{d as $,s as L,u as O,r as _,_ as x,e as f,o as I,c as j,f as u,m as r,x as g,a as h,t as w}from"./index-BSdrrqya.js";import{I as N,O as K,D as P}from"./style-CIU-wI-C.js";const V=$({components:{TableBody:N,TableHead:K,DataTable:P},setup(){const e=L(),i=O(),D=window.location.hostname,m=_([]),o=_({}),s=_({}),c=t=>t.target.parentNode.dataset.id,C=async t=>{let a=c(t);await e.push({name:"dashboard.masters.edit",params:{id:a}})},p=async t=>{if(!confirm("Вы точно хотите удалить запись?"))return;let d=c(t);await fetch(`/api/masters/${d}`,{method:"DELETE",credentials:"include"}).then(async n=>{const l=await n.json();l.message=="OK"?(i.success("Запись удалена"),b(void 0)):l.message=="BLOCK"?i.error("Удаление записи приведет к потере данных"):i.error("Ошибка при удалении записи")}).catch(n=>e.push({name:"dashboard"}))},b=async t=>{let a,d,n,l;n=s.value.sort,typeof t=="string"?(a=o.value.page,d=o.value.limit,n=n=="asc"?"desc":"asc",l=t.split(":")[0],l!==s.value.order&&(n="desc")):typeof t==typeof{}?(a=t.page-1<0?0:t.page-1,d=t.per_page,l=s.value.order):(a=o.value.page,d=o.value.limit,l=s.value.order);let B={page:a,limit:d,sort:n,order:l};var T=new URLSearchParams(B);await fetch(`/api/masters?${T}`,{headers:{"Content-Type":"application/json"},credentials:"include"}).then(async y=>{let k=await y.json();if(k.message="OK"){let v=k.data,E=v.max;a=v.page;let S=v.recs;m.value=S,o.value={...o.value,page:a,total:E,limit:d},s.value={...s.value,sort:n,order:l}}else m.value=[],o.value={...o.value,page:0,total:0,limit:d},s.value={...s.value,sort:n,order:l}}).catch(y=>e.push({name:"dashboard"}))};return{tableData:m,pagination:o,loadData:b,sort:s,delClick:p,editClick:C,hostname:D}}}),F=V,R=["src"],z=h("TableBodyCell",{class:"d-flex justify-content-center",colspan:"2"}," Нет записей ",-1);function H(e,i,D,m,o,s){const c=f("table-head"),C=f("router-link"),p=f("table-body"),b=f("data-table");return I(),j("div",null,[u(b,{rows:e.tableData,pagination:e.pagination,sort:e.sort,striped:"",onLoadData:e.loadData,style:{"font-size":"large !important"}},{thead:r(()=>[u(c,null,{default:r(()=>[g("Изображение")]),_:1}),u(c,{sortable:"FIO",onSorting:e.loadData},{default:r(()=>[g("ФИО")]),_:1},8,["onSorting"]),u(c,{sortable:"branch",onSorting:e.loadData},{default:r(()=>[g("Филиал")]),_:1},8,["onSorting"]),u(c,null,{default:r(()=>[u(C,{to:{name:"dashboard.masters.add"},tag:"button",type:"button",class:"btn btn-success"},{default:r(()=>[g("Добавить")]),_:1})]),_:1})]),tbody:r(({row:t})=>[u(p,null,{default:r(()=>[h("img",{src:`/resources/images/${t.photoPath}`,width:"150px",height:"150px"},null,8,R)]),_:2},1024),u(p,{textContent:w(t.fio)},null,8,["textContent"]),u(p,{textContent:w(t.address)},null,8,["textContent"]),u(p,{"data-id":t.masterId},{default:r(()=>[h("button",{type:"button",class:"btn btn-primary me-2",onClick:i[0]||(i[0]=(...a)=>e.editClick&&e.editClick(...a))},"ИЗМЕНИТЬ"),h("button",{type:"button",class:"btn btn-danger",onClick:i[1]||(i[1]=(...a)=>e.delClick&&e.delClick(...a))},"УДАЛИТЬ")]),_:2},1032,["data-id"])]),empty:r(()=>[z]),_:1},8,["rows","pagination","sort","onLoadData"])])}const A=x(F,[["render",H]]);export{A as default};
