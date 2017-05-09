# VersionInfo
以命令行的形式从git代码库中读取最后一次提交信息，输出到指定位置，可用于显示程序的版本信息
# 使用场景
在CI系统中（如bamboo），可以添加一个命令行任务，任务可以实现从指定的git库中读取最后一次提交时间，并另存到指定目录，程序可以读取这个信息，用于显示程序编辑时的版本。
# 命令行参数
## gitPath
指定的git库，可选（isReset=true时）。
## outPutPath
最后一次提交信息输出全路径，必填。
## format
输出格式，可选，默认为：--pretty=format:\" %cd@%H\" --date=iso8601
## isReset
可选，默认为false，可以设定为true，含义为指定生成一个空的文件到outPutPath

## 示例
gitPath=C:\Users\abc\bamboo-home\xml-data\build-dir\EBIDDING-PRRODUCT-JOB1\.git outPutPath=C:\CI\OriginalEXE\update.txt

将输出： 2017-05-08 17:55:19 +0800@b1e1dd1f1f3f702a35564c6abbb7cee72301b11b


