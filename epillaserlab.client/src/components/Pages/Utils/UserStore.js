export default {
  async user(){
    let responce = await fetch("/api/auth/user", {
      method: "GET",
      headers: {'Content-Type': "application/json"},
      credentials: "include",
    });

    var result = await responce.json();

    if(result.message == "OK"){
      return result.user;
    }

    return {};
  },

  isClient(user){ 
    return user && user.roleId == 3;
  }
}