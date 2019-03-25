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
 
    if(mysqli_num_rows($result) > 0)
    {
      echo "User already exists";
      }
      else
      {
        $sql  = "INSERT INTO users (username, email, password)
             VALUES ('".$username."','".$email."','".$password."')";
	  $result = mysqli_query($conn, $sql);

   
   
 

      }
        

    
?>