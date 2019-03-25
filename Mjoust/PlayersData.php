<?php
	  $servername = "localhost";
	  $server_username = "a0109350_users";
	  $server_password = "123456";
	  $dbName = "a0109350_users";

    $username = $_POST["usernamePost"];

	  $conn = new mysqli($servername, $server_username, $server_password, $dbName);

	if(!$conn){
	die("Connecttion Failed. " . mysqli_connection_error());
	}

	$sql  = "SELECT inventory, equip from users WHERE username = '".$username."' ";
	$result = mysqli_query($conn, $sql);

	if(mysqli_num_rows ($result) > 0)
  {
	while($row = mysqli_fetch_assoc($result))
	{
         // echo $username;
          echo $row['inventory'] . "|" .$row['equip'];
          
	}
	
	}


?>