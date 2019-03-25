  <?php
	  $servername = "localhost";
	  $server_username = "a0109350_users";
	  $server_password = "123456";
	  $dbName = "a0109350_users";
    
    $username = $_POST["usernamePost"];
    $score = $_POST["scorePost"];
    $inventory = $_POST["inventoryPost"];
    $equip = $_POST["equipPost"];
    $gold = $_POST["goldPost"];
    $progress = $_POST["progressPost"];

	  $conn = new mysqli($servername, $server_username, $server_password, $dbName);

	  if(!$conn){
	  die("Connecttion Failed. " . mysqli_connection_error());
	  }

	  
    $sql = "UPDATE users SET score = '".$score."', inventory = '".addslashes($inventory)."', equip = '".addslashes($equip)."', gold = '".$gold."', progress = '".$progress."' WHERE username = '".$username."'";
    
    $result = mysqli_query($conn, $sql);

	  if(!$result)
    {
	    echo "error";
	  }
    else 
    {
    echo "okay";
    }
    
?>