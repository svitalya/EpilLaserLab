export default {
  async user(){
    let responce = await fetch("/api/auth/user", {
      method: "GET",
      headers: {'Content-Type': "application/json"},
      credentials: "include",
    });

    return await responce.json();
  },

  isClient(user){
    if(user.message == "ACCESS DENIED" || !user.user) return false;

    return user.user.roleId == 3; 
  }
}