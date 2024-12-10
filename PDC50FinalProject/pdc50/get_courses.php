<?php
include 'db.php';

$sql = "SELECT DISTINCT Course FROM tblstudent";
$result = $conn->query($sql);

$courses = [];
while ($row = $result->fetch_assoc()) {
    $courses[] = $row['Course'];
}

echo json_encode($courses);
$conn->close();
?>