# SpanBenchmark
A quick .NET Core 2.1 application to examine the performance of the new Span&lt;T>

|Type|Iterations|Duration|
|----|----------|--------|
|Span|1000000|11ms|
|Array|1000000|9ms|
|Span|10000000|124ms|
|Array|10000000|102ms|
|Span|100000000|1160ms|
|Array|100000000|964ms|

|Type|Iterations|Duration|
|----|----------|--------|
|Span seek|1000000|17ms|
|Array seek|1000000|98ms|
|Span seek|10000000|81ms|
|Array seek|10000000|833ms|
|Span seek|100000000|800ms|
|Array seek|100000000|8212ms|
