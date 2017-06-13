Если у вас уже есть набор данных (массив или List), скорее всего вам нужно перемешивание его «на месте». Для этого подойдёт алгоритм из 3.4.2P из TAOCP, известный также как Fisher–Yates shuffle.

Пусть ваши данные находятся в массиве T[] data. Пусть random — экземпляр типа Random*. Тогда для перемешивания подходит следующий код:

for (int i = data.Length − 1; i >= 1; i--)
{
   int j = random.Next(i + 1);
   // обменять значения data[j] и data[i]
   var temp = data[j];
   data[j] = data[i];
   data[i] = temp;
}
Код очевидным образом адаптируется для случая List<T>.

Для случая, когда вам нужна не перетасовка на месте, а заполнение данными из другого источника, или данные генерируются на ходу (например, вы хотите получить перестановку чисел 1...n), можно воспользоваться немного модифицированным алгоритмом.

Если количество данных известно заранее (пусть это будет n), делаем так:

data = new T[n];
for (int i = 0; i < n; i++)
{
    int j = random.Next(i + 1);
    if (j != i)
        data[i] = data[j];
    data[j] = generate(i);
}
Здесь generate(i) — выражение, которое даёт следующий, i-ый член исходной последовательности. Например, если данные поступают из массива source, то generate(i) — это просто source[i]. Если вы перемешиваете числа от 1 до n, это просто i + 1, и т. д.

Для случая, когда количество элементов не известно заранее (например, из произвольного IEnumerable<T>), подойдёт следующая модификация. Наш целевой контейнер должен быть List<T>, чтобы его можно было динамически увеличивать.

data = new List<T>();
foreach (var s in source)
{
    int j = random.Next(data.Length + 1);
    if (j == data.Count)
    {
        data.Add(s);
    }
    else
    {
        data.Add(data[j]);
        data[j] = s;
    }
}
Код основан на цитированной статье из Википедии.