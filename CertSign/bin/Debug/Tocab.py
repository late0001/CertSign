import os
import sys

def fprintf(stream, format_spec, *args): 
    #print("="*80)
    stream.write(format_spec % args) 
def checkInf(path):
    idx = path.rfind("inf")
    if  idx == -1:
        return None
    print("Found INF: ", path)
    if os.path.exists(".\htm"): #清理之前遗留下来的旧档
        #os.rmdir(".\htm")
        for root, dirs, files in os.walk(".\htm", topdown=False):
            for name in files:
                os.remove(os.path.join(root, name))
            for name in dirs:
                os.rmdir(os.path.join(root, name))
    chkinf = "D:\\WinDDK\\7600.16385.0\\tools\\Chkinf\\chkinf.bat"
    os.system('%s \"%s\"' % (chkinf, path));
    
def addheader(filename):
    str = ''';*** Echo.ddf example
;
.OPTION EXPLICIT     ; Generate errors
.Set CabinetFileCountThreshold=0
.Set FolderFileCountThreshold=0
.Set FolderSizeThreshold=0
.Set MaxCabinetSize=0
.Set MaxDiskFileCount=0
.Set MaxDiskSize=0
.Set CompressionType=MSZIP
.Set Cabinet=on
.Set Compress=on
;Specify file name for new cab file
.Set CabinetNameTemplate=%s.cab
; Specify the subdirectory for the files.  
; Your cab file should not have files at the root level,
; and each driver package must be in a separate subfolder.
.Set DestinationDir=%s
;Specify files to be included in cab file
'''
    with open('test.ddf', 'w') as fp:
        fp.write(str % (filename,filename))
    
    
# 遍历文件夹
def walkFile(file):
    
    print("walkFile ==>", file)    
    with open('test.ddf', 'a+') as fp:
        for root, dirs, files in os.walk(file):
    
            # root 表示当前正在访问的文件夹路径
            # dirs 表示该文件夹下的子目录名list
            # files 表示该文件夹下的文件list
    
            # 遍历文件
            for f in files:
                path = os.path.join(root, f);
                #print(path)
                checkInf(path)
                fprintf(fp, "\"%s\"\n",path)
            # 遍历所有的文件夹
            for d in dirs:
                print(os.path.join(root, d))
            


if __name__ == '__main__':
    argc = len(sys.argv)
    adir=sys.argv[1]
    a=adir.rfind("\\")
    if(argc > 1):
        filename = sys.argv[2]
    else:        
        filename = adir[a+1:]
    addheader(filename)
    walkFile(adir)
    os.system('Makecab /f test.ddf')
    