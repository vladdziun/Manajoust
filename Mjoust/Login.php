  <?php
	  $servername = "localhost";
	  $server_username = "a0109350_users";
	  $server_password = "123456";
	  $dbName = "a0109350_users";
    
    $username = $_POST["usernamePost"];
    $email = $_POST["emailPost"];
    $password = $_POST["passwordPost"];

	  $conn = new mysqli($servername, $server_username, $server_password, $dbName);

	  if(!$conn){
	  die("Connecttion Failed. " . mysqli_connection_error());
	  }

	  $sql  = "SELECT password FROM users WHERE username = '".$username."' ";
	  $result = mysqli_query($conn, $sql);

	  //Get data and login
    if(mysqli_num_rows($result) > 0)
    {
      while($row = mysqli_fetch_assoc($result))
      {
        if($row['password'] == $password)
        {
          echo "login success";
          
        }
          else
          {
          echo "password incorrect";
          
          }
       }
    }
    else
    {
      echo "user not found";
    }
    
?>