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

    return null;
  },

  isClient(user){ 
    return user.roleId == 3;
  }
}