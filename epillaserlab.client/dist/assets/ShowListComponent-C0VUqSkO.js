import{d as E,s as L,u as $,r as D,_ as F,e as b,o as O,c as j,f as i,m as r,x as f,t as h,a as y}from"./index-BSdrrqya.js";import{I as x,O as I,D as N}from"./style-CIU-wI-C.js";const K=E({components:{TableBody:x,TableHead:I,DataTable:N},setup(){const t=L(),d=$(),m=D([]),s=D({}),n=D({}),g=e=>e.target.parentNode.dataset.id,p=async e=>{let a=g(e);await t.push({name:"dashboard.service.edit",params:{id:a}})},C=async e=>{if(!confirm("Вы точно хотите удалить запись?"))return;let o=g(e);await fetch(`/api/services/${o}`,{method:"DELETE",credentials:"include"}).then(async u=>{const l=await u.json();l.message=="OK"?(d.success("Запись удалена"),c(void 0)):l.message=="BLOCK"?d.error("Удаление записи приведет к потере данных"):d.error("Ошибка при удалении записи")}).catch(u=>t.push({name:"dashboard"}))},c=async e=>{let a,o,u,l;u=n.value.sort,typeof e=="string"?(a=s.value.page,o=s.value.limit,u=u=="asc"?"desc":"asc",l=e.split(":")[0],l!==n.value.order&&(u="desc")):typeof e==typeof{}?(a=e.page-1<0?0:e.page-1,o=e.per_page,l=n.value.order):(a=s.value.page,o=s.value.limit,l=n.value.order);let w={page:a,limit:o,sort:u,order:l};var B=new URLSearchParams(w);await fetch(`/api/services?${B}`,{headers:{"Content-Type":"application/json"},credentials:"include"}).then(async _=>{let k=await _.json();if(k.message="OK"){let v=k.data,S=v.max;a=v.page;let T=v.recs;m.value=T,s.value={...s.value,page:a,total:S,limit:o},n.value={...n.value,sort:u,order:l}}else m.value=[],s.value={...s.value,page:0,total:0,limit:o},n.value={...n.value,sort:u,order:l}}).catch(_=>t.push({name:"dashboard"}))};return{tableData:m,pagination:s,loadData:c,sort:n,delClick:C,editClick:p}}}),V=K,P=y("TableBodyCell",{class:"d-flex justify-content-center",colspan:"2"}," Нет записей ",-1);function R(t,d,m,s,n,g){const p=b("table-head"),C=b("router-link"),c=b("table-body"),e=b("data-table");return O(),j("div",null,[i(e,{rows:t.tableData,pagination:t.pagination,sort:t.sort,striped:"",onLoadData:t.loadData,style:{"font-size":"large !important"}},{thead:r(()=>[i(p,{sortable:"name",onSorting:t.loadData},{default:r(()=>[f("Название")]),_:1},8,["onSorting"]),i(p,{sortable:"description",onSorting:t.loadData},{default:r(()=>[f("Описание")]),_:1},8,["onSorting"]),i(p,{sortable:"timeCost",onSorting:t.loadData},{default:r(()=>[f("Время выполнения")]),_:1},8,["onSorting"]),i(p,{style:{width:"15%"}},{default:r(()=>[i(C,{to:{name:"dashboard.service.add"},tag:"button",type:"button",class:"btn btn-success"},{default:r(()=>[f("Добавить")]),_:1})]),_:1})]),tbody:r(({row:a})=>[i(c,{textContent:h(a.name)},null,8,["textContent"]),i(c,{textContent:h(a.description)},null,8,["textContent"]),i(c,{textContent:h(a.timeCost)},null,8,["textContent"]),i(c,{"data-id":a.serviceId,style:{width:"15%"}},{default:r(()=>[y("button",{type:"button",class:"btn btn-primary me-2",onClick:d[0]||(d[0]=(...o)=>t.editClick&&t.editClick(...o))},"ИЗМЕНИТЬ"),y("button",{type:"button",class:"btn btn-danger",onClick:d[1]||(d[1]=(...o)=>t.delClick&&t.delClick(...o))},"УДАЛИТЬ")]),_:2},1032,["data-id"])]),empty:r(()=>[P]),_:1},8,["rows","pagination","sort","onLoadData"])])}const J=F(V,[["render",R]]);export{J as default};
