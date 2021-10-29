import socket
import sys
from baidu import Spider

def start():
    startServer()
    
def startServer():
    address = ('127.0.0.1', 31500)
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    s.bind(address)
    s.listen(5)
    i=5
    while i>0:
        print('siguijiekege')
        #sys.stdout.flush()
        i-=1
    flag = 0
    while True:    
        ss, addr = s.accept()
        print('got connected from', addr)
        msg = 'welcome to visit me!'
        ss.send(msg.encode('utf-8'))
        while True:
            ra = ss.recv(512)
            print(ra,'\n')
            if ( len(ra) == 0):
                break
            elif( len(ra) >=3):
                if(ra[0] == 0xff ):
                    if (ra[1]== 0xff and ra[2]==0xfe):
                        flag=1
                        break
                elif(ra[0] == 0xE0):
                    pathlen = ra[1]
                    path = ra[2:2+pathlen]
                    spath = str(path, encoding = "utf-8")  
                    print("sign zip path=>", spath)
                    sign(spath)
        if flag == 1:
            break
        ss.close()
    s.close()

def sign(filename):
    a =Spider()
    a.signFiles(filename)
    
if __name__ == "__main__":
    start()