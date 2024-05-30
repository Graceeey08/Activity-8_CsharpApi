<?php
header("Content-Type: application/json");

$host = 'localhost';
$db = 'blissful_convenience'; // Change this to your actual database name
$user = 'root'; // Change this to your actual database user
$pass = ''; // Change this to your actual database password
$charset = 'utf8mb4';

$dsn = "mysql:host=$host;dbname=$db;charset=$charset";
$options = [
    PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
    PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
    PDO::ATTR_EMULATE_PREPARES => false,
];

$pdo = new PDO($dsn, $user, $pass, $options);

if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    $stmt = $pdo->query("SELECT product_id, productname, price, quantity FROM products");
    $products = $stmt->fetchAll();
    echo json_encode($products);
} elseif ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $input = json_decode(file_get_contents('php://input'), true);

    if (isset($input['productname'], $input['price'], $input['quantity'])) {
        $sql = "INSERT INTO products (productname, price, quantity) VALUES (?, ?, ?)";
        $stmt = $pdo->prepare($sql);
        $stmt->execute([
            $input['productname'], 
            $input['price'], 
            $input['quantity']
        ]);
        echo json_encode(['message' => 'Product added successfully']);
    } else {
        echo json_encode(['error' => 'Invalid input', 'received' => $input]);
    }
}
?>
